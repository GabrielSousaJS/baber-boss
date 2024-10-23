namespace BarberBoss.Communication.User.Responses;

public class ResponseUserProfileJson
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
}
