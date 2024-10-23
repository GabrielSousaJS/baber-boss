namespace BarberBoss.Communication.User.Requests;

public class RequestUpdateUserJson
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
}
