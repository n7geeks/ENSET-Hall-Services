namespace ENSETHall.ScolarityService.Models;

public record MajorDetails(
	string Major,
	string MajorFullName,
	string Department,
	string Diploma,
	string Formation
)
{
    public override string ToString() => 
        $"{{major:{Major},majorFullName:'{MajorFullName}',department:{Department},diploma:{Diploma},formation:{Formation}}}";
    public static implicit operator string(MajorDetails majorDetails) => majorDetails.ToString();
    
}