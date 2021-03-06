using System;
using System.Collections.Generic;
using NHibernate.Engine;
using NHibernate.SqlCommand;

namespace NHibernate.Criterion
{
	/// <summary>
	/// An <see cref="ICriterion"/> that represents "null" constraint.
	/// </summary>
	[Serializable]
	public class NullExpression : AbstractCriterion
	{
		private readonly string _propertyName;
		private readonly IProjection _projection;
		private static readonly TypedValue[] NoValues = new TypedValue[0];

		/// <summary>
		/// Initializes a new instance of the <see cref="NullExpression"/> class.
		/// </summary>
		/// <param name="projection">The projection.</param>
		public NullExpression(IProjection projection)
		{
			_projection = projection;
		}

		/// <summary>
		/// Initialize a new instance of the <see cref="NotNullExpression" /> class for a named
		/// Property that should be null.
		/// </summary>
		/// <param name="propertyName">The name of the Property in the class.</param>
		public NullExpression(string propertyName)
		{
			_propertyName = propertyName;
		}

		public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
		{
			//TODO: add default capacity
			SqlStringBuilder sqlBuilder = new SqlStringBuilder();

			SqlString[] columnNames =
				CriterionUtil.GetColumnNames(_propertyName, _projection, criteriaQuery, criteria, enabledFilters);

			for (int i = 0; i < columnNames.Length; i++)
			{
				if (i > 0)
				{
					sqlBuilder.Add(" and ");
				}

				sqlBuilder.Add(columnNames[i])
					.Add(" is null");
			}

			if (columnNames.Length > 1)
			{
				sqlBuilder.Insert(0, "(");
				sqlBuilder.Add(")");
			}

			return sqlBuilder.ToSqlString();
		}

		public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return _projection == null ? NoValues : _projection.GetTypedValues(criteria, criteriaQuery);
		}

		public override IProjection[] GetProjections()
		{
			if(_projection != null)
			{
				return new IProjection[] { _projection };
			}
			return null;
		}

		/// <summary></summary>
		public override string ToString()
		{
			return (_projection ?? (object)_propertyName) + " is null";
		}
	}
}
