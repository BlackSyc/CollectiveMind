using System;
using System.Collections.Generic;

namespace CollectiveMind.Graph.Edges
{
	public abstract class Edge<T>
	{
		public virtual Guid Id { get; set; }
		
		public int TotalCount { get; set; }
		
		public List<T> Nodes { get; set; }
	}
}