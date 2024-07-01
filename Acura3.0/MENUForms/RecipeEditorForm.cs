using AcuraLibrary;
using AcuraLibrary.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace Acura3._0.MENUForms
{
    public partial class RecipeEditorForm : Form
    {
        public RecipeEditorForm()
        {
            InitializeComponent();
        }

        private void RecipeEditorForm_Load(object sender, EventArgs e)
        {
            tcRecipeEditor.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plRecipeEditor.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plRecipeEditor.Parent = tg;
                    tcRecipeEditor.TabPages.Add(tg);
                }
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            bool isSuccess = false;
            try
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    if (ModuleManager.ModuleList[i].bIsRecipeClean)
                    {
                        isSuccess = true;
                        ModuleManager.ModuleList[i].BeforRecipeEditor();
                    }

                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    if (ModuleManager.ModuleList[i].bIsRecipeClean)
                    {
                        isSuccess = true;
                        ModuleManager.ModuleList[i].WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                    }

                if(isSuccess)

                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    if (ModuleManager.ModuleList[i].bIsRecipeClean)
                    {
                        isSuccess = true;
                        ModuleManager.ModuleList[i].AfterRecipeEditor();
                    }

                if (isSuccess)
                    MessageBox.Show(new Form { TopMost = true }, "Saved successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, "ERROR: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            MiddleLayer.OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            MiddleLayer.StartUp();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDir = new SaveFileDialog();
            SaveFileDir.Filter = "XML Files|*.xml";

            string Directory = SysPara.RecipeDataDirectory.Replace(".\\", System.IO.Directory.GetCurrentDirectory() + "\\");
            SaveFileDir.InitialDirectory = Directory;
            if (SaveFileDir.ShowDialog() == DialogResult.OK)
            {
                if (ModuleManager.ModuleList.Count > 0)
                {
                    ModuleManager.ModuleList[0].WriteRecipeData(SaveFileDir.FileName);
                    SysPara.RecipeDataDirectory = Path.GetDirectoryName(SaveFileDir.FileName);
                    SysPara.RecipeName = Path.GetFileNameWithoutExtension(SaveFileDir.FileName);
                    MiddleLayer.OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                }
                MessageBox.Show(new Form { TopMost = true }, "Created new recipe successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            string OrgRecipeName = SysPara.RecipeName;
            OpenFileDialog OpenFileDir = new OpenFileDialog();
            OpenFileDir.Filter = "XML Files|*.xml";

            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
            {
                if (ModuleManager.ModuleList[i].bIsRecipeClean)
                    ModuleManager.ModuleList[i].BeforRecipeEditor();
            }

            try
            {
                OpenFileDir.InitialDirectory = SysPara.RecipeDataDirectory.Replace(".\\", System.IO.Directory.GetCurrentDirectory() + "\\");
            }
            catch (Exception)
            {
                SysPara.RecipeDataDirectory = string.Format("{0}\\ModuleData\\RecipeData\\Recipe.xml", System.IO.Directory.GetCurrentDirectory());
                string directory = Path.GetDirectoryName(SysPara.RecipeDataDirectory);
                System.IO.Directory.CreateDirectory(directory);
                OpenFileDir.InitialDirectory = directory;
            }

            if (OpenFileDir.ShowDialog() == DialogResult.OK)
            {
                if (MiddleLayer.OpenRecipe(OpenFileDir.FileName))
                {
                    //MiddleLayer.LogF.AddLog(LogType.Operation, string.Format("User change the recipe \"{0}\"->\"{1}\" . UserType:{2} UserName:{3}", OrgRecipeName, SysPara.RecipeName, SysPara.LoginLevel.ToString(), SysPara.LoginUserName));
                }
                MiddleLayer.StartUp();
                SysPara.SystemRun = false;
                MiddleLayer.SetModulesStop();
                SysPara.SystemMode = RunMode.IDLE;
                SysPara.SystemInitialOk = false;
            }

            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
            {
                if (ModuleManager.ModuleList[i].bIsRecipeClean)
                    ModuleManager.ModuleList[i].AfterRecipeEditor();
            }
          
        }

        private void RecipeEditorForm_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null)
            {
                MiddleLayer.StopManualRun();
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ExitRecipeEditorPage();
            }
            else
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].IntoRecipeEditorPage();
            }
        }
    }
}
