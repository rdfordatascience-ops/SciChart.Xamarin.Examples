﻿using Android.Views.Animations;
using SciChart.Charting.Model;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Modifiers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Animations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Drawing.Common;
using Xamarin.Examples.Demo.Data;
using Xamarin.Examples.Demo;
using Xamarin.Examples.Demo.Droid.Extensions;
using Xamarin.Examples.Demo.Droid.Fragments.Base;

namespace Xamarin.Examples.Demo.Droid.Fragments.Examples
{
    [ExampleDefinition("Chart Legends API", description:"Demonstrates the SciChart Legend API", icon: ExampleIcon.LineChart)]
    public class LegendFragment : ExampleBaseFragment
    {
        public override int ExampleLayoutId => Resource.Layout.Example_Single_Chart_Fragment;

        private SciChartSurface Surface => View.FindViewById<SciChartSurface>(Resource.Id.chart);

        protected override void InitExample()
        {
            var xAxis = new NumericAxis(Activity);
            var yAxis = new NumericAxis(Activity);

            var ds1 = new XyDataSeries<double, double> {SeriesName = "Curve A"};
            var ds2 = new XyDataSeries<double, double> {SeriesName = "Curve B"};
            var ds3 = new XyDataSeries<double, double> {SeriesName = "Curve C"};
            var ds4 = new XyDataSeries<double, double> {SeriesName = "Curve D"};

            var ds1Points = DataManager.Instance.GetStraightLine(4000, 1.0, 10);
            var ds2Points = DataManager.Instance.GetStraightLine(3000, 1.0, 10);
            var ds3Points = DataManager.Instance.GetStraightLine(2000, 1.0, 10);
            var ds4Points = DataManager.Instance.GetStraightLine(1000, 1.0, 10);

            ds1.Append(ds1Points.XData, ds1Points.YData);
            ds2.Append(ds2Points.XData, ds2Points.YData);
            ds3.Append(ds3Points.XData, ds3Points.YData);
            ds4.Append(ds4Points.XData, ds4Points.YData);

            var legendModifier = new LegendModifier(Activity);
            legendModifier.SetSourceMode(SourceMode.AllSeries);

            var line1 = new FastLineRenderableSeries { DataSeries = ds1, StrokeStyle = new SolidPenStyle(0xFFFFFF00, 2f.ToDip(Activity)) };
            var line2 = new FastLineRenderableSeries { DataSeries = ds2, StrokeStyle = new SolidPenStyle(0xFF279B27, 2f.ToDip(Activity)) };
            var line3 = new FastLineRenderableSeries { DataSeries = ds3, StrokeStyle = new SolidPenStyle(0xFFFF1919, 2f.ToDip(Activity)) };
            var line4 = new FastLineRenderableSeries { DataSeries = ds4, IsVisible = false, StrokeStyle = new SolidPenStyle(0xFF1964FF, 2f.ToDip(Activity)) };

            using (Surface.SuspendUpdates())
            {
                Surface.XAxes.Add(xAxis);
                Surface.YAxes.Add(yAxis);
                Surface.RenderableSeries = new RenderableSeriesCollection { line1, line2, line3, line4 };
                Surface.ChartModifiers.Add(legendModifier);

                new SweepAnimatorBuilder(line1) { Interpolator = new DecelerateInterpolator(), Duration = 3000, StartDelay = 350 }.Start();
                new SweepAnimatorBuilder(line2) { Interpolator = new DecelerateInterpolator(), Duration = 3000, StartDelay = 350 }.Start();
                new SweepAnimatorBuilder(line3) { Interpolator = new DecelerateInterpolator(), Duration = 3000, StartDelay = 350 }.Start();
                new SweepAnimatorBuilder(line4) { Interpolator = new DecelerateInterpolator(), Duration = 3000, StartDelay = 350 }.Start();
            }
        }
    }
}