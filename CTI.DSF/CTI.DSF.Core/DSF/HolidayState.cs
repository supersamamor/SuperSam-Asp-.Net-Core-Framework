using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record HolidayState : BaseEntity
{
	public string HolidayName { get; init; } = "";
	public DateTime HolidayDate { get; init; }
	
	
	
}
