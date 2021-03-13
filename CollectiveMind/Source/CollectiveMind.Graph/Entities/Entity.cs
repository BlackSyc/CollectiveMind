using System;

namespace CollectiveMind.Graph.Entities
{
	/// <summary>
	/// Abstract representation of any stored entity within a graph.
	/// </summary>
	public abstract class Entity
	{
		/// <summary>
		/// The identifier of the stored entity.
		/// </summary>
		public virtual Guid Id { get; set; }
	}
}