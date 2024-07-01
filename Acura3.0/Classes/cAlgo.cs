using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Acura3._0.Classes
{
    public static class cAlgo
    {
        #region Circle
        public static List<Point> DrawCirclePoints(int points, double radius, Point center)
        {
            List<Point> lpoints = new List<Point>();
            double slice = 2 * Math.PI / points;
            for (int i = 0; i < points; i++)
            {
                double angle = slice * i;
                int newX = (int)(center.X + radius * Math.Cos(angle));
                int newY = (int)(center.Y + radius * Math.Sin(angle));
                Point p = new Point(newX, newY);
                lpoints.Add(p);
                Console.WriteLine(p);
            }

            return lpoints;
        }

        public static bool isInsideCircle(PointF center, double rad, double x, double y)
        {
            if ((x - center.X) * (x - center.X) +
                (y - center.Y) * (y - center.Y) <= rad * rad)
                return true;
            else
                return false;
        }
        #endregion


        public static double[] algoMovingAvg(int frameSize, int frameStart, double[] Profile)
        {
            double sum = 0;
            double[] avgPoints = new double[Profile.Length];

            if (frameSize > Profile.Length) return Profile;
            if (frameStart < 0) return Profile;
            for (int counter = 0; counter < Profile.Length; counter++)
            {
                if (counter >= frameStart && counter <= frameStart+ frameSize && counter< Profile.Length-1)
                {
                    int innerLoopCounter = 0;
                    int index = counter;
                    while (innerLoopCounter < frameSize)
                    {
                        sum = sum + Profile[index];

                        innerLoopCounter += 1;

                        index += 1;

                    }

                    avgPoints[counter] = sum / frameSize;

                    sum = 0;
                }
                else
                {
                    avgPoints[counter] = Profile[counter];
                }
            }
            return avgPoints;

        }

        public static (double, int) algoFindPeak(double[] Profile)
        {
            double maxValue = Profile.Max();
            int maxIndex = Profile.ToList().IndexOf(maxValue);

            return (maxValue, maxIndex);
        }

        public static (double, int) algoFindNadir(double[] Profile)
        {
            double minValue = Profile.Min();
            int minIndex = Profile.ToList().IndexOf(minValue);

            return (minValue, minIndex);
        }

        public static double algoCalculateOffSet(double[] Profile, int windowSize = 2)
        {
            var _peakProfile = algoMovingAvg(windowSize, algoFindPeak(Profile).Item2, Profile);
            var _minProfile = algoMovingAvg(windowSize, algoFindNadir(_peakProfile).Item2, _peakProfile);

            double offset = algoFindPeak(_minProfile).Item1 - algoFindNadir(_minProfile).Item1;
            return offset;
        }

        public static double algoCalculateOffSetEnd(double[] Profile,int startSkip, int endSkip,  int windowSize = 2)
        {
            if (startSkip >= Profile.Length) return -999;
            if (endSkip >= Profile.Length) return -999;
            if(windowSize >= Profile.Length/3) return -999;
            var _peakProfile = algoMovingAvg(windowSize, Profile.Length- endSkip, Profile);
            var _minProfile = algoMovingAvg(windowSize, startSkip, _peakProfile);

            if (startSkip > 0) startSkip--;
            double offset = _minProfile[Profile.Length-1 - endSkip] - _minProfile[startSkip];
            return offset;
        }
    }
}
