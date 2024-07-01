using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acura3._0.Classes
{
    public class CsvFile
    {

        public void writeResult(string path, string CSVname, string Barcode,double dome11Height, double dome12Height,
            double dome11Vision, double dome12Vision,bool HResult,bool VResult)
        {
            string filePath = path + "\\" + CSVname + "_" + DateTime.Now.ToString("(yyyyMMdd)") + ".csv";
            var csv = new StringBuilder();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(filePath))
            {
               // File.Create(filePath);
                File.WriteAllText(filePath, Convert.ToString(csv));
                csv.Append("No,DateTime,Barcode,Dome 1 Height,Dome 2 Height,Height Result,Dome 1 Diameter,Dome 2 Diameter,Vision Result" + "\n");
            }

            string[] arr = File.ReadAllLines(filePath);
            int count = arr.Length;
            if (count == 0)
            {
                count = 1;
            }
            foreach (string prevData in arr)
            {
                var newLine = string.Format("{0}{1}", prevData, Environment.NewLine);
                csv.Append(newLine);
            }
            try
            {
                csv.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    count.ToString(),DateTime.Now.ToString("HH:mm:ss"), Barcode, dome11Height, dome12Height, HResult, dome11Vision, dome12Vision, VResult));
            }
            catch (Exception ed)
            {
                Console.WriteLine(ed.Message);
            }

            File.WriteAllText(filePath, Convert.ToString(csv));
        }

        public void writeHeightResult(string path, string CSVname,string dome,List<double> data)
        {
            string filePath = path + "\\" + CSVname + "_" + DateTime.Now.ToString("(yyyyMMdd)") + ".csv";
            var csv = new StringBuilder();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(filePath))
            {
                // File.Create(filePath);
                File.WriteAllText(filePath, Convert.ToString(csv));
                csv.Append("No,DateTime,Dome,Point A,Point B,Point C,Point D,Point E,Point F,Point G,Point H" + "\n");
            }

            string[] arr = File.ReadAllLines(filePath);
            int count = arr.Length;
            if (count == 0)
            {
                count = 1;
            }

            foreach (string prevData in arr)
            {
                var newLine = string.Format("{0}{1}", prevData, Environment.NewLine);
                csv.Append(newLine);
            }
            try
            {
                if (data.Count != 8)
                    return;
                csv.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                    count.ToString(), DateTime.Now.ToString("HH:mm:ss"), dome, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]));
            }
            catch (Exception ed)
            {
                Console.WriteLine(ed.Message);
            }

            File.WriteAllText(filePath, Convert.ToString(csv));
        }

        public void writeVisionResult(string path, string CSVname, string dome, double Diameter,double Score)
        {
            string filePath = path + "\\" + CSVname + "_" + DateTime.Now.ToString("(yyyyMMdd)") + ".csv";
            var csv = new StringBuilder();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(filePath))
            {
                // File.Create(filePath);
                File.WriteAllText(filePath, Convert.ToString(csv));
                csv.Append("No,DateTime,Dome,Diameter,Score" + "\n");
            }

            string[] arr = File.ReadAllLines(filePath);
            int count = arr.Length;
            if (count == 0)
            {
                count = 1;
            }

            foreach (string prevData in arr)
            {
                var newLine = string.Format("{0}{1}", prevData, Environment.NewLine);
                csv.Append(newLine);
            }
            try
            {
              
                csv.Append(string.Format("{0},{1},{2},{3},{4}",
                    count.ToString(), DateTime.Now.ToString("HH:mm:ss"), dome, Diameter, Score));
            }
            catch (Exception ed)
            {
                Console.WriteLine(ed.Message);
            }

            File.WriteAllText(filePath, Convert.ToString(csv));
        }


    }
}
