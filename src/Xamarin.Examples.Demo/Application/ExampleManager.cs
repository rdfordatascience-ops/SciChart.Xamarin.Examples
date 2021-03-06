﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Xamarin.Examples.Demo
{
    public class ExampleManager
    {
        private static ExampleManager _exampleManager;

        public static ExampleManager Instance => _exampleManager ?? (_exampleManager = new ExampleManager());

        public List<Example> Examples { get; }
        public List<Example> Examples3D { get; }
        public List<Example> FeaturedExamples { get; }

        private ExampleManager()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().ToList();

            Examples = types.Where(t => Attribute.IsDefined(t, typeof(ExampleDefinition))).Select(t => new Example(t)).OrderBy(ex => ex.Title).ToList();
            Examples3D = types.Where(t => Attribute.IsDefined(t, typeof(Example3DDefinition))).Select(t => new Example(t)).OrderBy(ex => ex.Title).ToList();
            FeaturedExamples = types.Where(t => Attribute.IsDefined(t, typeof(FeaturedExampleDefinition))).Select(t => new Example(t)).OrderBy(ex => ex.Title).ToList();
        }

        public Example GetExampleByTitle(string exampleTitle, string categoryId)
        {
            var examples = GetExamplesByCategory(categoryId);

            return examples.FirstOrDefault(x => x.Title == exampleTitle);
        }

        public List<Example> GetExamplesByCategory(string categoryId)
        {
            if (categoryId == DemoKeys.Featured)
                return FeaturedExamples;

            if (categoryId == DemoKeys.Charts3D)
                return Examples3D;

            return Examples;
        }
    }
}