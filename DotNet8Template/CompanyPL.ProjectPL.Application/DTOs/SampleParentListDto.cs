using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Application.DTOs;

public record SampleParentListDto : BaseDto
{
	public string Name { get; init; } = ""; 
	
	
}
