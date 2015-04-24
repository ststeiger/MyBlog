
#if false

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Drawing;
using System.Web.UI.DataVisualization.Charting;


namespace MyBlog.Controllers
{


    public class ChartsController : Controller
    {


        //
        // GET: /Charts/
        public ActionResult Index()
        {
            return RedirectToAction("Test");
            //return View();
        }


        public ActionResult Test()
        {
            return View();
        }


        // http://stackoverflow.com/questions/6047961/c-sharp-chart-rotate-labels
        public FileResult RadarSample()
        {
            
            int pixelWidth = 1000;
            int pixelHeight = 1000;

            // Populate series data
            //string[] xValues = { "France", "Canada", "Germany", "USA", "Italy", "Spain", "Russia", "Sweden", "Japan" };

            string[] xValues = { "Offene\nAussenpolitik", "Liberale\nWirtschaftspolitik", "Restriktive\nFinanzpolitik", "Law & Order", "Restriktive\nMigrationspolitik", "Ausgebauter\nUmweltschutz", "Ausgebauter\nSozialstaat", "Liberale\nGesellschaft" };
            double[] yValues = { 80, 90, 45, 75, 37.5, 40, 28, 54 };


            //double[] yValues = { 65.62, 75.54, 60.45, 34.73, 85.42, 55.9, 63.6, 55.1, 77.2 };
            //double[] yValues2 = { 76.45, 23.78, 86.45, 30.76, 23.79, 35.67, 89.56, 67.45, 38.98 };
            
            
            var Chart1 = new System.Web.UI.DataVisualization.Charting.Chart();
            

            var area = new System.Web.UI.DataVisualization.Charting.ChartArea("ca1");
            area.Area3DStyle.Enable3D = false;
            //area.AxisX.Interval = 1;


            Chart1.BackColor = System.Drawing.Color.HotPink;
            Chart1.BackColor = System.Drawing.Color.White;

            area.BackColor = System.Drawing.Color.Red;
            area.BackColor = System.Drawing.Color.Transparent;

            //area.AxisY.Interval = 5;

            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.Gray;
            area.AxisY.MajorGrid.Interval = 25;

            area.AxisY.MinorTickMark.Enabled = false;
            area.AxisY.MinorGrid.Interval = 5;
            area.AxisY.MinorGrid.LineColor = Color.Yellow;

            Chart1.ChartAreas.Add(area);
            

            var series1 = new System.Web.UI.DataVisualization.Charting.Series();
            var series2 = new System.Web.UI.DataVisualization.Charting.Series();
            
            series1.Name = "Series1";
            series2.Name = "Series2";

            //series1.Color = System.Drawing.Color.Yellow;
            series1.Color = System.Drawing.Color.FromArgb(100, 84, 130, 255);
            series1.SmartLabelStyle.Enabled = true;
            //series1.LabelAngle = 90;

            //Legend legend = new Legend();

            ////legend.Name = "mylegend";
            //legend.Title = "Hello world";
            //legend.BackColor = Color.Transparent;
            //legend.BackColor = Color.Tomato;

            //Chart1.Legends.Add(legend);

            
            // series1.Legend = "mylegend";
             series1.LegendText = "A";
             series2.LegendText = "B";

             // series1.Label = "kickme";
            // series2.Label = "bar";

            //series1.ChartArea = "ca1";

            series1.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Radar;
            series2.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Radar;

            series1.ChartArea = "ca1";
            series2.ChartArea = "ca1";


            Chart1.Series.Add(series1);
            //Chart1.Series.Add(series2);


            Chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
            //Chart1.Series["Series2"].Points.DataBindXY(xValues, yValues2);

            Chart1.Series["Series1"].SmartLabelStyle.Enabled = false;
            // Chart1.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;
            // Chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 30;

            Chart1.ChartAreas[0].Axes[0].CustomLabels.Add((double)0, (double)0, "hello");
            Chart1.ChartAreas[0].Axes[0].CustomLabels.Add((double)1, (double)0, "bar");

            

            for (int i = 0; i < Chart1.ChartAreas[0].Axes[0].CustomLabels.Count; ++i)
            {
                
                Chart1.ChartAreas[0].Axes[0].CustomLabels[0].Axis.TextOrientation = TextOrientation.Rotated90;    
            }

            for (int i = 0; i < Chart1.Series["Series1"].Points.Count; ++i)
            {
                Chart1.Series["Series1"].Points[i].LabelAngle = -30;
                Chart1.Series["Series1"].Points[i].LabelBackColor = System.Drawing.Color.Turquoise;
                //Chart1.Series["Series1"].Points[i].AxisLabel = "This works";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                
            }
            

            /*
                foreach (DataPoint dp2 in Chart1.Series["Series1"].Points)
                {
                    Console.WriteLine(dp2);
                    //series1.SmartLabelStyle.Enabled = true;

                    dp2.LabelAngle = 30;
                }

            DataPoint dp = new DataPoint(0, 0);
            dp.AxisLabel = "hollavballoo";
            dp.LabelAngle = 30;
            */

            //series1.Points.Add(dp);





            string[] astrRadarStyleList = new string[] { "Area", "Line", "Marker" }; // Fill, Line, or point
            string[] astrAreaDrawingStyleList = new string[] { "Circle", "Polygon" }; // Shape
            string[] astrLabelStyleList = new string[] { "Circular", "Radial", "Horizontal" };



            string strRadarStyle = astrRadarStyleList[0];
            string strAreaDrawingStyle = astrAreaDrawingStyleList[0];
            string strLabelStyle = astrLabelStyleList[0];
            

            Chart1.Width = System.Web.UI.WebControls.Unit.Pixel(pixelWidth);
            Chart1.Height = System.Web.UI.WebControls.Unit.Pixel(pixelHeight);


            // Set radar chart style
            Chart1.Series["Series1"]["RadarDrawingStyle"] = strRadarStyle; // RadarStyleList.SelectedItem.Text;
            //Chart1.Series["Series2"]["RadarDrawingStyle"] = strRadarStyle; // RadarStyleList.SelectedItem.Text;




            if (strRadarStyle == "Area")
            {
                Chart1.Series["Series1"].BorderColor = Color.FromArgb(100, 100, 100);
                Chart1.Series["Series1"].BorderWidth = 1;
                // Chart1.Series["Series2"].BorderColor = Color.FromArgb(100, 100, 100);
                // Chart1.Series["Series2"].BorderWidth = 1;
            }
            else if (strRadarStyle == "Line")
            {
                Chart1.Series["Series1"].BorderColor = Color.Empty;
                Chart1.Series["Series1"].BorderWidth = 2;
                // Chart1.Series["Series2"].BorderColor = Color.Empty;
                // Chart1.Series["Series2"].BorderWidth = 2;
            }
            else if (strRadarStyle == "Marker")
            {
                Chart1.Series["Series1"].BorderColor = Color.Empty;
                // Chart1.Series["Series2"].BorderColor = Color.Empty;
            }

            // Set circular area drawing style
            Chart1.Series["Series1"]["AreaDrawingStyle"] = strAreaDrawingStyle; // AreaDrawingStyleList.SelectedItem.Text;
            //Chart1.Series["Series2"]["AreaDrawingStyle"] = strAreaDrawingStyle; // AreaDrawingStyleList.SelectedItem.Text;

            

            // Set labels style
            //Chart1.Series["Series1"]["CircularLabelsStyle"] = strLabelStyle; // LabelStyleList.SelectedItem.Text;
            //Chart1.Series["Series2"]["CircularLabelsStyle"] = strLabelStyle; //LabelStyleList.SelectedItem.Text;

            return Chart2Image(Chart1);
        }


        public FileResult Chart2Image(System.Web.UI.DataVisualization.Charting.Chart chart)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                chart.SaveImage(ms, System.Web.UI.DataVisualization.Charting.ChartImageFormat.Png);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png", "mychart.png");
            } // End Using ms
        }




        public FileResult ChartSample()
        {
            var chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.BackColor = System.Drawing.Color.Transparent;
            chart.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);

            chart.Width = System.Web.UI.WebControls.Unit.Pixel(250);
            chart.Height = System.Web.UI.WebControls.Unit.Pixel(2500);

            var series = new System.Web.UI.DataVisualization.Charting.Series();
            series.ChartArea = "ca1";



            series.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Bar;
            //series.Font = new Font("Verdana", 8.25f, FontStyle.Regular);

            System.Random myRandom = new System.Random();

            for (int i = 0; i < 100; i++)
            {
                var dp = new System.Web.UI.DataVisualization.Charting.DataPoint();
                dp.AxisLabel = String.Format("{0}-{1}", i, Guid.NewGuid().ToString().Substring(0, 4));
                dp.YValues = new double[] { myRandom.Next(5, 100) };
                series.Points.Add(dp);
            } // Next i


            chart.Series.Add(series);

            var area = new System.Web.UI.DataVisualization.Charting.ChartArea("ca1");
            area.Area3DStyle.Enable3D = false;
            area.AxisX.Interval = 1;
            //area.BackColor = Color.Transparent;
            //var labelStyle = new LabelStyle();
            //labelStyle.Enabled = true;
            //labelStyle.Font = new Font("Arial", 3f);
            area.AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Underline);//Why does it recognize the style but not the font!!!???

            area.BackColor = System.Drawing.Color.Red;
            //area.BackColor = System.Drawing.Color.White;

            chart.ChartAreas.Add(area);

            /*
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                chart.SaveImage(ms, System.Web.UI.DataVisualization.Charting.ChartImageFormat.Png);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png", "mychart.png");
            } // End Using ms
            */
            return Chart2Image(chart);
        } // End Action ChartSample 


    }


}

#endif
