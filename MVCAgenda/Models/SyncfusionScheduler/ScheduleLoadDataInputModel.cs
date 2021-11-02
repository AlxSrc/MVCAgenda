using Newtonsoft.Json;
using System;

namespace MVCAgenda.Models.SyncfusionScheduler
{
	public class ScheduleLoadDataInputModel
	{
		public ScheduleLoadDataInputModel()
		{
			Value = new ScheduleLoadDataInputValueModel();
		}

		[JsonProperty(PropertyName = "value")]
		public ScheduleLoadDataInputValueModel Value { get; set; }

		public class ScheduleLoadDataInputValueModel
		{
			public DateTime? StartDate { get; set; }
			public DateTime? EndDate { get; set; }
		}
	}
}
