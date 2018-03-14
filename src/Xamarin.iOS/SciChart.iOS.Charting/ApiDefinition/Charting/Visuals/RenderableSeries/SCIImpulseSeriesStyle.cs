﻿using Foundation;
using ObjCRuntime;

namespace SciChart.iOS.Charting
{
    // @interface SCIImpulseSeriesStyle : NSObject <SCIStyleProtocol, NSCopying>
    [BaseType(typeof(NSObject))]
    interface SCIImpulseSeriesStyle : SCIStyleProtocol, INSCopying
    {
        // @property (nonatomic, strong) SCIPenStyle * strokeStyle;
        [Export("strokeStyle", ArgumentSemantic.Strong)]
        SCIPenStyle StrokeStyle { get; set; }

        // @property (nonatomic, strong) id<SCIPointMarkerProtocol> pointMarker;
        [Export("pointMarker", ArgumentSemantic.Strong)]
        ISCIPointMarkerProtocol PointMarker { get; set; }

        // @property (nonatomic, strong) SCIPointMarkerClusterizer * cluster;
        [Export("cluster", ArgumentSemantic.Strong)]
        SCIPointMarkerClusterizer Cluster { get; set; }

        // @property (nonatomic) float clusterSpacing;
        [Export("clusterSpacing")]
        float ClusterSpacing { get; set; }
    }
}