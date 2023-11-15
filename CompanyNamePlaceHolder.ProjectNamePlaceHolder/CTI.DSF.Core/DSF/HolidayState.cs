using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record HolidayState : BaseEntity
{
	public DateTime HolidayDate { get; init; }
	public string HolidayName { get; init; } = "";
	
	
	
}
