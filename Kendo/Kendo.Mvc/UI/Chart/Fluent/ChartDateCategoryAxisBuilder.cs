namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.Resources;
    using Kendo.Mvc.UI;

    /// <summary>
    /// Configures date category axis for the <see cref="Chart{TModel}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item to which the chart is bound to</typeparam>
    public class ChartDateCategoryAxisBuilder<TModel> : ChartAxisBuilderBase<IChartCategoryAxis, int, ChartDateCategoryAxisBuilder<TModel>>
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartCategoryAxisBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public ChartDateCategoryAxisBuilder(Chart<TModel> chart)
            : base(chart.CategoryAxis)
        {
            Container = chart;
            Container.CategoryAxis.Type = ChartCategoryAxisType.Date;
        }

        /// <summary>
        /// The parent Chart
        /// </summary>
        public Chart<TModel> Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines bound categories.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the categories value from the chart model
        /// </param>
        public ChartDateCategoryAxisBuilder<TModel> Categories(Expression<Func<TModel, DateTime>> expression)
        {
            if (typeof(TModel).IsPlainType() && !expression.IsBindable())
            {
                throw new InvalidOperationException(Exceptions.MemberExpressionRequired);
            }

            var value = expression.Compile();

            if (Container.Data != null)
            {
                var dataList = new List<DateTime?>();

                foreach (var dataPoint in Container.Data)
                {
                    dataList.Add(dataPoint != null ? value(dataPoint) : new Nullable<DateTime>());
                }

                Container.CategoryAxis.Categories = dataList;
            }
            else
            {
                Container.CategoryAxis.Member = expression.MemberWithoutInstance();
            }

            return this;
        }

        /// <summary>
        /// Defines categories.
        /// </summary>
        /// <param name="categories">
        /// The list of categories
        /// </param>
        public ChartDateCategoryAxisBuilder<TModel> Categories(IEnumerable<DateTime> categories)
        {
            Container.CategoryAxis.Categories = categories;

            return this;
        }

        /// <summary>
        /// Defines categories.
        /// </summary>
        /// <param name="categories">
        /// The list of categories
        /// </param>
        public ChartDateCategoryAxisBuilder<TModel> Categories(params DateTime[] categories)
        {
            Container.CategoryAxis.Categories = categories;

            return this;
        }

        /// <summary>
        /// Sets the date category axis base unit.
        /// </summary>
        /// <param name="baseUnit">
        /// The date category axis base unit
        /// </param>
        public ChartDateCategoryAxisBuilder<TModel> BaseUnit(ChartAxisBaseUnit baseUnit)
        {
            Container.CategoryAxis.BaseUnit = baseUnit;

            return this;
        }

        /// <summary>
        /// Sets the date category axis minimum (start) date.
        /// </summary>
        /// <param name="min">
        /// The date category axis minimum (start) date
        /// </param>
        public ChartDateCategoryAxisBuilder<TModel> Min(DateTime min)
        {
            Container.CategoryAxis.Min = min;

            return this;
        }

        /// <summary>
        /// Sets the date category axis maximum (end) date.
        /// </summary>
        /// <param name="max">
        /// The date category axis maximum (end) date
        /// </param>
        public ChartDateCategoryAxisBuilder<TModel> Max(DateTime max)
        {
            Container.CategoryAxis.Max = max;

            return this;
        }

        /// <summary>
        /// Sets value at which the first perpendicular axis crosses this axis.
        /// </summary>
        /// <param name="axisCrossingValue">The value at which the first perpendicular axis crosses this axis.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart(Model)
        ///            .Name("Chart")
        ///            .CategoryAxis(axis => axis.Date().AxisCrossingValue(4))
        ///            .ValueAxis(axis => axis.Numeric().Title("Axis 1"))
        ///            .ValueAxis(axis => axis.Numeric("secondary").Title("Axis 2"))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartDateCategoryAxisBuilder<TModel> AxisCrossingValue(double axisCrossingValue)
        {
            Axis.AxisCrossingValues = new double[] { axisCrossingValue };

            return this;
        }

        /// <summary>
        /// Sets value at which perpendicular axes cross this axis.
        /// </summary>
        /// <param name="axisCrossingValues">The values at which perpendicular axes cross this axis.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart(Model)
        ///            .Name("Chart")
        ///            .CategoryAxis(axis => axis.Date().AxisCrossingValue(0, 10))
        ///            .ValueAxis(axis => axis.Numeric().Title("Axis 1"))
        ///            .ValueAxis(axis => axis.Numeric("secondary").Title("Axis 2"))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartDateCategoryAxisBuilder<TModel> AxisCrossingValue(params double[] axisCrossingValues)
        {
            Axis.AxisCrossingValues = axisCrossingValues;

            return this;
        }

        /// <summary>
        /// Sets value at which perpendicular axes cross this axis.
        /// </summary>
        /// <param name="axisCrossingValues">The values at which perpendicular axes cross this axis.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart(Model)
        ///            .Name("Chart")
        ///            .CategoryAxis(axis => axis.Date().AxisCrossingValue(new double[] { 0, 10 }))
        ///            .ValueAxis(axis => axis.Numeric().Title("Axis 1"))
        ///            .ValueAxis(axis => axis.Numeric("secondary").Title("Axis 2"))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartDateCategoryAxisBuilder<TModel> AxisCrossingValue(IEnumerable<double> axisCrossingValues)
        {
            Axis.AxisCrossingValues = axisCrossingValues;

            return this;
        }

        /// <summary>
        /// Configures the axis labels.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Kendo().Chart()
        ///            .Name("Chart")
        ///            .CategoryAxis(axis => axis
        ///                .Date()
        ///                .Labels(labels => labels
        ///                    .Culture(new CultureInfo("es-ES")
        ///                    .Visible(true)
        ///                );
        ///            )
        /// %&gt;
        /// </code>
        /// </example>
        public ChartDateCategoryAxisBuilder<TModel> Labels(Action<ChartDateAxisLabelsBuilder> configurator)
        {
            configurator(new ChartDateAxisLabelsBuilder(Axis.Labels));

            return this;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ChartDateCategoryAxisBuilder<TModel> Labels(Action<ChartAxisLabelsBuilder> configurator)
        {
            return this;
        }
    }
}
