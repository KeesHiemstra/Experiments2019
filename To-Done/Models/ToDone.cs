using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Done.Models
{
	public class ToDone
	{
		public int Id { get; set; }
		public string Subject { get; set; }
		public DateTime? DoneTime { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? DueDate { get; set; }
		public StatusLevel Status { get; set; } = StatusLevel.NotStarted;
		public PriortyLevel Priority { get; set; } = PriortyLevel.Normal;
	}

	public enum StatusLevel
	{
		NotStarted = 0,
		Waiting = 1,
		InProgress = 10,
		Completed = 99
	}

	public enum PriortyLevel
	{
		Low = -9,
		Decreased = -5,
		Normal = 0,
		Increased = 5,
		High = 9
	}

}
