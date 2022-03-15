namespace Core.Domain.Entities;

public class NicknameAssignment
{
    public int NicknameAssignmentId { get; set; }
    public int RequesterId { get; set; }
    public int AddresseeId{ get; set; }
    public string NicknameValue { get; set; }

    public User Requester { get; set; }
    public User Addressee { get; set; }
}