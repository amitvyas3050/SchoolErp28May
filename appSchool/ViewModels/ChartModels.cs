using appSchool.Repositories;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraCharts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace appSchool.ChartModels
{


    public class ChartShowLabelsDemoOptions : ChartViewTypeDemoOptions
    {
        bool showLabels;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }

        public ChartShowLabelsDemoOptions()
        {
        }
    }
    public class ChartBarViewsDemoOptions : ChartShowLabelsDemoOptions
    {
        bool rotated;

        public bool Rotated
        {
            get { return rotated; }
            set { rotated = value; }
        }

        public ChartBarViewsDemoOptions()
        {
        }
    }

    public class ChartPieDoughnutViewsDemoOptions : ChartViewTypeDemoOptions
    {
        static List<string> listExplodeModes = new List<string>(){
            PieExplodeMode.None.ToString(),
            PieExplodeMode.All.ToString(),
            PieExplodeMode.MinValue.ToString(),
            PieExplodeMode.MaxValue.ToString()
        };

        bool showLabels = true;
        bool valueAsPercent = true;
        string explodedPoints = PieExplodeMode.None.ToString();
        string explodePoint;
        PieExplodeMode explodeMode = PieExplodeMode.None;
        PieSeriesLabelPosition labelPosition = PieSeriesLabelPosition.Radial;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }
        public bool ValueAsPercent
        {
            get { return valueAsPercent; }
            set { valueAsPercent = value; }
        }
        public PieSeriesLabelPosition LabelPosition
        {
            get { return labelPosition; }
            set { labelPosition = value; }
        }
        public string ExplodedPoints
        {
            get { return explodedPoints; }
            set
            {
                explodedPoints = value;
                if (listExplodeModes.Contains(value))
                {
                    explodePoint = null;
                    explodeMode = (PieExplodeMode)Enum.Parse(typeof(PieExplodeMode), value);
                }
                else
                    explodePoint = value;
            }
        }
        public string ExplodePoint { get { return explodePoint; } }
        public PieExplodeMode ExplodeMode { get { return explodeMode; } }

        public ChartPieDoughnutViewsDemoOptions()
        {
            View = DevExpress.XtraCharts.ViewType.Pie;
        }
    }

    public class ChartFunnelViewsDemoOptions : ChartViewTypeDemoOptions
    {
        bool showLabels = true;
        bool valueAsPercent = true;
        FunnelSeriesLabelPosition labelPosition = FunnelSeriesLabelPosition.Right;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }
        public bool ValueAsPercent
        {
            get { return valueAsPercent; }
            set { valueAsPercent = value; }
        }
        public FunnelSeriesLabelPosition LabelPosition
        {
            get { return labelPosition; }
            set { labelPosition = value; }
        }

        public ChartFunnelViewsDemoOptions()
        {
            View = DevExpress.XtraCharts.ViewType.Funnel;
        }
    }

    public class ChartNestedDoughnutViewDemoOptions : ChartViewTypeDemoOptions
    {
        bool showAgeStruct = true;
        int holeRadiusPercent = 40;
        double innerIndent = 5;

        public bool ShowAgeStruct
        {
            get { return showAgeStruct; }
            set { showAgeStruct = value; }
        }
        public int HoleRadiusPercent
        {
            get { return holeRadiusPercent; }
            set { holeRadiusPercent = value; }
        }
        public double InnerIndent
        {
            get { return innerIndent; }
            set { innerIndent = value; }
        }

        public ChartNestedDoughnutViewDemoOptions()
        {
            View = DevExpress.XtraCharts.ViewType.Bar;
        }
    }

    public class ChartRadarPolarViewsDemoOptions : ChartViewTypeDemoOptions
    {
        bool showLabels = true;
        int starPointCount = 3;
        int markerSize = 8;
        string markerKind = MarkerKind.Circle.ToString();
        RadarDiagramDrawingStyle diagramStyle = RadarDiagramDrawingStyle.Circle;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }
        public int MarkerSize
        {
            get { return markerSize; }
            set { markerSize = value; }
        }
        public string MarkerKindString
        {
            get { return markerKind; }
            set
            {
                if (value == null)
                    markerKind = MarkerKind.Circle.ToString();
                else if (value.Contains(ChartDemoHelper.StarKey))
                {
                    starPointCount = Int32.Parse(value.Remove(0, ChartDemoHelper.StarKey.Length));
                    markerKind = MarkerKind.Star.ToString();
                }
                else
                    markerKind = value;
            }
        }
        public RadarDiagramDrawingStyle DiagramStyle
        {
            get { return diagramStyle; }
            set { diagramStyle = value; }
        }
        public int StarPointCount { get { return starPointCount; } }

        public ChartRadarPolarViewsDemoOptions()
        {
            View = DevExpress.XtraCharts.ViewType.RadarPoint;
        }
    }

    public class ChartFinacialViewsDemoOptions : ChartViewTypeDemoOptions
    {
        bool workDaysOnly = true;
        bool showLabels = false;
        StockLevel reductionLevel = StockLevel.Close;
        StockLevel labelLevel = StockLevel.Close;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }
        public bool WorkDaysOnly
        {
            get { return workDaysOnly; }
            set { workDaysOnly = value; }
        }
        public StockLevel ReductionLevel
        {
            get { return reductionLevel; }
            set { reductionLevel = value; }
        }
        public StockLevel LabelLevel
        {
            get { return labelLevel; }
            set { labelLevel = value; }
        }

        public ChartFinacialViewsDemoOptions()
        {
            View = DevExpress.XtraCharts.ViewType.Stock;
        }
    }

    public class ChartSeriesBindingDemoOptions
    {
        public const string DefaultCategory = "Confections";

        bool showLabels = true;
        string category = DefaultCategory;
        SeriesPointKey sortingKey = SeriesPointKey.Value_1;
        SortingMode sortingMode = SortingMode.Ascending;
        IEnumerable categories;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public SeriesPointKey SortingKey
        {
            get { return sortingKey; }
            set { sortingKey = value; }
        }
        public SortingMode SortingMode
        {
            get { return sortingMode; }
            set { sortingMode = value; }
        }
        public IEnumerable Categories { get { return categories; } }

        public ChartSeriesBindingDemoOptions()
        {
            // categories = NorthwindDataProvider.GetCategoriesNames();
        }
    }

    public class ChartSeriesTemplateBindingDemoOptions
    {
        public const string Region = "Region";
        public const string Year = "Year";

        bool showLabels;
        string seriesDataMember = Year;

        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }
        public string SeriesDataMember
        {
            get { return seriesDataMember; }
            set { seriesDataMember = string.IsNullOrEmpty(value) ? Year : value; }
        }
        public string ArgumentDataMember { get { return seriesDataMember == Region ? Year : Region; } }

        public ChartSeriesTemplateBindingDemoOptions()
        {
        }
    }
    public class ChartViewTypeDemoOptions
    {
        DevExpress.XtraCharts.ViewType view;

        public DevExpress.XtraCharts.ViewType View
        {
            get { return view; }
            set { view = value; }
        }

        public ChartViewTypeDemoOptions()
        {
        }
    }

    public static class ChartDemoHelper
    {
        public const string OptionsKey = "Options";
        public const string CategoryKey = "Category";
        public const string StarKey = "star:";
        public const string CompletedDateKey = "CompletedDate";
        public const string ModelKey = "Model";

        public static bool IsSideBySideStackedView(DevExpress.XtraCharts.ViewType view)
        {
            return view == DevExpress.XtraCharts.ViewType.SideBySideStackedBar
                || view == DevExpress.XtraCharts.ViewType.SideBySideStackedBar3D
                || view == DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar
                || view == DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar3D;
        }
        public static bool IsPolarView(DevExpress.XtraCharts.ViewType view)
        {
            return view == DevExpress.XtraCharts.ViewType.PolarArea
                || view == DevExpress.XtraCharts.ViewType.PolarLine
                || view == DevExpress.XtraCharts.ViewType.PolarPoint;
        }
        public static List<SelectListItem> GetBarViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Bar", Value = DevExpress.XtraCharts.ViewType.Bar.ToString() },
                new SelectListItem() { Text = "Stacked Bar", Value = DevExpress.XtraCharts.ViewType.StackedBar.ToString() },
                new SelectListItem() { Text = "Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.FullStackedBar.ToString() },
                new SelectListItem() { Text = "Side-by-Side Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideStackedBar.ToString() },
                new SelectListItem() { Text = "Side-by-Side Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar.ToString() },
                new SelectListItem() { Text = "3D Bar", Value = DevExpress.XtraCharts.ViewType.Bar3D.ToString() },
                new SelectListItem() { Text = "3D Manhattan Bar", Value = DevExpress.XtraCharts.ViewType.ManhattanBar.ToString() },
                new SelectListItem() { Text = "3D Stacked Bar", Value = DevExpress.XtraCharts.ViewType.StackedBar3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.FullStackedBar3D.ToString() },
                new SelectListItem() { Text = "3D Side-by-Side Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideStackedBar3D.ToString() },
                new SelectListItem() { Text = "3D Side-by-Side Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar3D.ToString() }
            };
        }
        public static List<SelectListItem> GetPointLineViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Point", Value = DevExpress.XtraCharts.ViewType.Point.ToString() },
                new SelectListItem() { Text = "Bubble", Value = DevExpress.XtraCharts.ViewType.Bubble.ToString() },
                new SelectListItem() { Text = "Line", Value = DevExpress.XtraCharts.ViewType.Line.ToString() },
                new SelectListItem() { Text = "Scatter Line", Value = DevExpress.XtraCharts.ViewType.ScatterLine.ToString() },
                new SelectListItem() { Text = "Step Line", Value = DevExpress.XtraCharts.ViewType.StepLine.ToString() },
                new SelectListItem() { Text = "Stacked Line", Value = DevExpress.XtraCharts.ViewType.StackedLine.ToString() },
                new SelectListItem() { Text = "Full-Stacked Line", Value = DevExpress.XtraCharts.ViewType.FullStackedLine.ToString() },
                new SelectListItem() { Text = "Spline", Value = DevExpress.XtraCharts.ViewType.Spline.ToString() },
                new SelectListItem() { Text = "3D Line", Value = DevExpress.XtraCharts.ViewType.Line3D.ToString() },
                new SelectListItem() { Text = "3D Step Line", Value = DevExpress.XtraCharts.ViewType.StepLine3D.ToString() },
                new SelectListItem() { Text = "3D Stacked Line", Value = DevExpress.XtraCharts.ViewType.StackedLine3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Line", Value = DevExpress.XtraCharts.ViewType.FullStackedLine3D.ToString() },
                new SelectListItem() { Text = "3D Spline", Value = DevExpress.XtraCharts.ViewType.Spline3D.ToString() }
            };
        }
        public static List<SelectListItem> GetAreaViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Area", Value = DevExpress.XtraCharts.ViewType.Area.ToString() },
                new SelectListItem() { Text = "Stacked Area", Value = DevExpress.XtraCharts.ViewType.StackedArea.ToString() },
                new SelectListItem() { Text = "Full-Stacked Area", Value = DevExpress.XtraCharts.ViewType.FullStackedArea.ToString() },
                new SelectListItem() { Text = "Spline Area", Value = DevExpress.XtraCharts.ViewType.SplineArea.ToString() },
                new SelectListItem() { Text = "Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.StackedSplineArea.ToString() },
                new SelectListItem() { Text = "Full-Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.FullStackedSplineArea.ToString() },
                new SelectListItem() { Text = "Step Area", Value = DevExpress.XtraCharts.ViewType.StepArea.ToString() },
                new SelectListItem() { Text = "3D Area", Value = DevExpress.XtraCharts.ViewType.Area3D.ToString() },
                new SelectListItem() { Text = "3D Stacked Area", Value = DevExpress.XtraCharts.ViewType.StackedArea3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Area", Value = DevExpress.XtraCharts.ViewType.FullStackedArea3D.ToString() },
                new SelectListItem() { Text = "3D Spline Area", Value = DevExpress.XtraCharts.ViewType.SplineArea3D.ToString() },
                new SelectListItem() { Text = "3D Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.StackedSplineArea3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.FullStackedSplineArea3D.ToString() },
                new SelectListItem() { Text = "3D Step Area", Value = DevExpress.XtraCharts.ViewType.StepArea3D.ToString() },
            };
        }
        public static List<SelectListItem> GetPieViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Pie", Value = DevExpress.XtraCharts.ViewType.Pie.ToString() },
                new SelectListItem() { Text = "Doughnut", Value = DevExpress.XtraCharts.ViewType.Doughnut.ToString() },
                new SelectListItem() { Text = "3D Pie", Value = DevExpress.XtraCharts.ViewType.Pie3D.ToString() },
                new SelectListItem() { Text = "3D Doughnut", Value = DevExpress.XtraCharts.ViewType.Doughnut3D.ToString() },
            };
        }
        public static List<SelectListItem> GetPieLabelPositions()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Radial", Value = PieSeriesLabelPosition.Radial.ToString() },
                new SelectListItem() { Text = "Inside", Value = PieSeriesLabelPosition.Inside.ToString() },
                new SelectListItem() { Text = "Outside", Value = PieSeriesLabelPosition.Outside.ToString() },
                new SelectListItem() { Text = "Two Columns", Value = PieSeriesLabelPosition.TwoColumns.ToString() },
            };
        }
        public static List<SelectListItem> GetPieExplodePoints()
        {
            List<SelectListItem> explodePoints = new List<SelectListItem>() {
                new SelectListItem() { Text = "None", Value = PieExplodeMode.None.ToString() },
                new SelectListItem() { Text = "All", Value = PieExplodeMode.All.ToString() },
                new SelectListItem() { Text = "Min Value", Value = PieExplodeMode.MinValue.ToString() },
                new SelectListItem() { Text = "Max Value", Value = PieExplodeMode.MaxValue.ToString() },
            };
            //foreach (Country country in CountriesProvider.GetCountries())
            //    explodePoints.Add(new SelectListItem() { Text = country.Name, Value = country.Name });
            return explodePoints;
        }
        public static List<SelectListItem> GetFunnelViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Funnel", Value = DevExpress.XtraCharts.ViewType.Funnel.ToString() },
                new SelectListItem() { Text = "3D Funnel", Value = DevExpress.XtraCharts.ViewType.Funnel3D.ToString() },
            };
        }
        public static List<SelectListItem> GetFunnelLabelPositions()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Right", Value = FunnelSeriesLabelPosition.Right.ToString() },
                new SelectListItem() { Text = "Left", Value = FunnelSeriesLabelPosition.Left.ToString() },
                new SelectListItem() { Text = "Center", Value = FunnelSeriesLabelPosition.Center.ToString() },
                new SelectListItem() { Text = "Right Column", Value = FunnelSeriesLabelPosition.RightColumn.ToString() },
                new SelectListItem() { Text = "Left Column", Value = FunnelSeriesLabelPosition.LeftColumn.ToString() },
            };
        }
        public static List<SelectListItem> GetRadarPolarViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Radar Point", Value = DevExpress.XtraCharts.ViewType.RadarPoint.ToString() },
                new SelectListItem() { Text = "Radar Line", Value = DevExpress.XtraCharts.ViewType.RadarLine.ToString() },
                new SelectListItem() { Text = "Radar Area", Value = DevExpress.XtraCharts.ViewType.RadarArea.ToString() },
                new SelectListItem() { Text = "Polar Point", Value = DevExpress.XtraCharts.ViewType.PolarPoint.ToString() },
                new SelectListItem() { Text = "Polar Line", Value = DevExpress.XtraCharts.ViewType.PolarLine.ToString() },
                new SelectListItem() { Text = "Polar Area", Value = DevExpress.XtraCharts.ViewType.PolarArea.ToString() }
            };
        }
        public static List<SelectListItem> GetMarkerKinds()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Circle", Value = DevExpress.XtraCharts.MarkerKind.Circle.ToString() },
                new SelectListItem() { Text = "Cross", Value = DevExpress.XtraCharts.MarkerKind.Cross.ToString() },
                new SelectListItem() { Text = "Diamond", Value = DevExpress.XtraCharts.MarkerKind.Diamond.ToString() },
                new SelectListItem() { Text = "Hexagon", Value = DevExpress.XtraCharts.MarkerKind.Hexagon.ToString() },
                new SelectListItem() { Text = "Inverted Triangle", Value = DevExpress.XtraCharts.MarkerKind.InvertedTriangle.ToString() },
                new SelectListItem() { Text = "Pentagon", Value = DevExpress.XtraCharts.MarkerKind.Pentagon.ToString() },
                new SelectListItem() { Text = "Plus", Value = DevExpress.XtraCharts.MarkerKind.Plus.ToString() },
                new SelectListItem() { Text = "Square", Value = DevExpress.XtraCharts.MarkerKind.Square.ToString() },
                new SelectListItem() { Text = "Triangle", Value = DevExpress.XtraCharts.MarkerKind.Triangle.ToString() },
                new SelectListItem() { Text = "Star 3-points", Value = StarKey + "3" },
                new SelectListItem() { Text = "Star 4-points", Value = StarKey + "4" },
                new SelectListItem() { Text = "Star 5-points", Value = StarKey + "5" },
                new SelectListItem() { Text = "Star 6-points", Value = StarKey + "6" },
                new SelectListItem() { Text = "Star 10-points", Value = StarKey + "10" }
            };
        }
        public static List<SelectListItem> GetRangeViews()
        {
            return new List<SelectListItem>(){
                new SelectListItem() { Text = "Range Bar", Value = DevExpress.XtraCharts.ViewType.RangeBar.ToString() },
                new SelectListItem() { Text = "Side-by-Side Range Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideRangeBar.ToString() },
                new SelectListItem() { Text = "Range Area", Value = DevExpress.XtraCharts.ViewType.RangeArea.ToString() },
                new SelectListItem() { Text = "3D Range Area", Value = DevExpress.XtraCharts.ViewType.RangeArea3D.ToString() },
            };
        }
        public static List<SelectListItem> GetGanttViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Gantt", Value = DevExpress.XtraCharts.ViewType.Gantt.ToString() },
                new SelectListItem() { Text = "Side-by-Side Gantt", Value = DevExpress.XtraCharts.ViewType.SideBySideGantt.ToString() }
            };
        }
        public static List<SelectListItem> GetFinancialViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Stock", Value = DevExpress.XtraCharts.ViewType.Stock.ToString() },
                new SelectListItem() { Text = "Candle Stick", Value = DevExpress.XtraCharts.ViewType.CandleStick.ToString() }
            };
        }
        public static List<SelectListItem> GetFinancialLabelLevels()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Close", Value = StockLevel.Close.ToString() },
                new SelectListItem() { Text = "High", Value = StockLevel.High.ToString() },
                new SelectListItem() { Text = "Low", Value = StockLevel.Low.ToString() },
                new SelectListItem() { Text = "Open", Value = StockLevel.Open.ToString() }
            };
        }
        public static List<SelectListItem> GetSortValues()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Products", Value = SeriesPointKey.Argument.ToString() },
                new SelectListItem() { Text = "Price", Value = SeriesPointKey.Value_1.ToString(), Selected = true }
            };
        }
        public static List<SelectListItem> GetSeriesDataMembers()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = ChartSeriesTemplateBindingDemoOptions.Year, Value = ChartSeriesTemplateBindingDemoOptions.Year, Selected = true },
                new SelectListItem() { Text = ChartSeriesTemplateBindingDemoOptions.Region, Value = ChartSeriesTemplateBindingDemoOptions.Region }
            };
        }
        public static List<SelectListItem> GetRadarDiagramTypes()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Circle", Value = RadarDiagramDrawingStyle.Circle.ToString(), Selected = true },
                new SelectListItem() { Text = "Polygon", Value = RadarDiagramDrawingStyle.Polygon.ToString() }
            };
        }
        public static List<SelectListItem> GetAgeStructureModes()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Age", Value = "true", Selected = true },
                new SelectListItem() { Text = "Sex", Value = "false" }
            };
        }
        public static List<SelectListItem> GetPredefinedHoleRadiuses()
        {
            const int minRadius = 30;
            const int step = 5;
            const int itemsCount = 8;
            const int defaultRadius = 40;
            List<SelectListItem> items = new List<SelectListItem>();
            for (int itemIndex = 0; itemIndex < itemsCount; itemIndex++)
            {
                int radius = minRadius + step * itemIndex;
                string radiusString = radius.ToString();
                items.Add(new SelectListItem()
                {
                    Text = radiusString,
                    Value = radiusString,
                    Selected = radius == defaultRadius
                });
            }
            return items;
        }
        public static List<SelectListItem> GetPredefinedInnerIndents()
        {
            const int itemsCount = 11;
            const int defaultIndentIndex = 5;
            List<SelectListItem> items = new List<SelectListItem>();
            for (int itemIndex = 0; itemIndex < itemsCount; itemIndex++)
            {
                string indent = itemIndex.ToString();
                items.Add(new SelectListItem()
                {
                    Text = indent,
                    Value = indent,
                    Selected = itemIndex == defaultIndentIndex
                });
            }
            return items;
        }
        public static List<string> GetExportFormats()
        {
            return new List<string>() { "pdf", "xls", "xlsx", "rtf", "mht", "png", "jpeg", "bmp", "tiff", "gif" };
        }
        public static List<int> GetMarkerSizes()
        {
            return new List<int>() { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
        }
    }


   
}






