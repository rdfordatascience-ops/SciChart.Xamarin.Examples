﻿using System.Timers;
using Android.App;
using System.Drawing;
using Android.OS;
using Android.Views;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Modifiers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Core.Framework;
using SciChart.Core.Model;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using Math = System.Math;

namespace Tutorial06_AddRealtimeUpdates
{
    [Activity(Label = "Xamarin.Android.Tutorial", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set license key before using SciChart
            SciChartSurface.SetRuntimeLicenseKey("");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our chart from the layout resource,
            var chart = FindViewById<SciChartSurface>(Resource.Id.Chart);

            // Create a numeric X axis
            var xAxis = new NumericAxis(this) { AxisTitle = "Number of Samples (per Series)" };

            // Create a numeric Y axis
            var yAxis = new NumericAxis(this)
            {
                AxisTitle = "Value",
                VisibleRange = new DoubleRange(-1, 1)
            };

            // Add xAxis to the XAxes collection of the chart
            chart.XAxes.Add(xAxis);

            // Add yAxis to the YAxes collection of the chart
            chart.YAxes.Add(yAxis);

            // Create XyDataSeries to host data for our chart
            var lineData = new XyDataSeries<double, double>() { SeriesName = "Sin(x)" };
            var scatterData = new XyDataSeries<double, double>() { SeriesName = "Cos(x)" };

            // Append data which should be drawn
            for (var i = 0; i < 1000; i++)
            {
                lineData.Append(i, Math.Sin(i * 0.1));
                scatterData.Append(i, Math.Cos(i * 0.1));
            }

            double phase = 0;

            var timer = new Timer(30) { AutoReset = true };
            var lineBuffer = new DoubleValues(1000);
            var scatterBuffer = new DoubleValues(1000);

            // Update on each tick of timer
            timer.Elapsed += (s, e) =>
            {
                lineBuffer.Clear();
                scatterBuffer.Clear();

                for (int i = 0; i < 1000; i++)
                {
                    lineBuffer.Add(Math.Sin(i * 0.1 + phase));
                    scatterBuffer.Add(Math.Cos(i * 0.1 + phase));
                }

                using (chart.SuspendUpdates())
                {
                    lineData.UpdateRangeYAt(0, lineBuffer);
                    scatterData.UpdateRangeYAt(0, scatterBuffer);
                }

                phase += 0.01;
            };

            timer.Start();

            var lineSeries = new FastLineRenderableSeries()
            {
                DataSeries = lineData,
                StrokeStyle = new SolidPenStyle(Color.LightBlue, 2)
            };

            // Create scatter series with data appended into scatterData
            var scatterSeries = new XyScatterRenderableSeries()
            {
                DataSeries = scatterData,
                PointMarker = new EllipsePointMarker()
                {
                    Width = 10,
                    Height = 10,
                    StrokeStyle = new SolidPenStyle(Color.Green, 2),
                    FillStyle = new SolidBrushStyle(Color.LightBlue)
                }
            };

            // Add the renderable series to the RenderableSeries collection of the chart
            chart.RenderableSeries.Add(lineSeries);
            chart.RenderableSeries.Add(scatterSeries);

            // Create interactivity modifiers
            var pinchZoomModifier = new PinchZoomModifier();
            pinchZoomModifier.SetReceiveHandledEvents(true);

            var zoomPanModifier = new ZoomPanModifier();
            zoomPanModifier.SetReceiveHandledEvents(true);

            var zoomExtentsModifier = new ZoomExtentsModifier();
            zoomExtentsModifier.SetReceiveHandledEvents(true);

            var yAxisDragModifier = new YAxisDragModifier();
            yAxisDragModifier.SetReceiveHandledEvents(true);

            // Create and configure legend
            var legendModifier = new LegendModifier(this);
            legendModifier.SetLegendPosition(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 10);
            legendModifier.SetOrientation(Orientation.Horizontal);

            // Create RolloverModifier to show tooltips
            var rolloverModifier = new RolloverModifier();
            rolloverModifier.SetReceiveHandledEvents(true);

            // Create modifier group from declared modifiers
            var modifiers = new ModifierGroup(pinchZoomModifier, zoomPanModifier, zoomExtentsModifier, yAxisDragModifier, rolloverModifier, legendModifier);

            // Add the interactions to the ChartModifiers collection of the chart
            chart.ChartModifiers.Add(modifiers);
        }
    }
}

