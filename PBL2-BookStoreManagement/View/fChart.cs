using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fChart : Form
    {
        public fChart(string title, Dictionary<string, double> data, string chartType)
        {
            InitializeComponent();

            this.Text = title;
            this.Size = new Size(1200, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Tạo Panel để chứa biểu đồ
            Panel chartPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            // Tạo biểu đồ LiveCharts
            var chart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Chuẩn bị dữ liệu
            IEnumerable<KeyValuePair<string, double>> processedData;

            if (chartType == "Line")
            {
                processedData = data
                    .Where(kvp => DateTime.TryParse(kvp.Key, out _))
                    .OrderByDescending(kvp => DateTime.Parse(kvp.Key))
                    .Take(7)
                    .OrderBy(kvp => DateTime.Parse(kvp.Key));
            }
            else
            {
                processedData = data
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(5)
                    .OrderBy(kvp => kvp.Value);
            }

            var labels = processedData.Select(kvp => kvp.Key).ToList();
            var values = processedData.Select(kvp => kvp.Value).ToArray();
            double maxVal = values.Max();

            var seriesCollection = new SeriesCollection();

            if (chartType == "Line")
            {
                seriesCollection.Add(new LineSeries
                {
                    Title = "Revenue:",
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    Values = new ChartValues<double>(values),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 67, 54)),
                    DataLabels = true,
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelPoint = point => point.Y.ToString("N2") + " $",
                });

                chart.AxisY.Add(new Axis
                {
                    Title = "Revenue ($) ",
                    LabelFormatter = val => val.ToString("N0"),
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    MinValue = 0,
                    MaxValue = maxVal * 1.1,
                    Separator = new Separator
                    {
                        Step = Math.Ceiling(maxVal / 5),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                        StrokeDashArray = new System.Windows.Media.DoubleCollection { 2, 2 }
                    },
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                });

                chart.AxisX.Add(new Axis
                {
                    Title = "",
                    Labels = labels,
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    LabelsRotation = 0,
                    Separator = new Separator
                    {
                        Step = 1,
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
                    },
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                });
            }
            else if (title == "Top Selling Products")
            {
                seriesCollection.Add(new ColumnSeries
                {
                    Title = "Quantity: ",
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    Values = new ChartValues<double>(values),
                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 67, 54)),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                    StrokeThickness = 1,
                    DataLabels = true,
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelPoint = point => point.Y.ToString("N0"),
                });

                chart.AxisY.Add(new Axis
                {
                    Title = "Quantity",
                    LabelFormatter = val => val.ToString("N0"),
                    MinValue = 0,
                    MaxValue = maxVal * 1.1,
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    Separator = new Separator
                    {
                        Step = Math.Ceiling(maxVal / 5),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                        StrokeDashArray = new System.Windows.Media.DoubleCollection { 2, 2 }
                    },
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                });

                chart.AxisX.Add(new Axis
                {
                    Title = "",
                    Labels = labels,
                    LabelsRotation = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    Separator = new Separator
                    {
                        Step = 1,
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
                    },
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                });
            }
            else
            {
                seriesCollection.Add(new ColumnSeries
                {
                    Title = "Total Spending: ",
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    Values = new ChartValues<double>(values),
                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 67, 54)),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                    StrokeThickness = 1,
                    DataLabels = true,
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelPoint = point => point.Y.ToString("N2") + "$",
                });

                chart.AxisY.Add(new Axis
                {
                    Title = "Total Spending ($)",
                    LabelFormatter = val => val.ToString("N0") + "$",
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    MinValue = 0,
                    MaxValue = maxVal * 1.1,
                    Separator = new Separator
                    {
                        Step = Math.Ceiling(maxVal / 5),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                        StrokeDashArray = new System.Windows.Media.DoubleCollection { 2, 2 }
                    },
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                });

                chart.AxisX.Add(new Axis
                {
                    Title = "",
                    Labels = labels,
                    LabelsRotation = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                    Separator = new Separator
                    {
                        Step = 1,
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
                    },
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                });
            }

            chart.Series = seriesCollection;

            // Thêm biểu đồ vào panel và panel vào form
            chartPanel.Controls.Add(chart);
            this.Controls.Add(chartPanel);
        }
    }
}
