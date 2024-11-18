using System;

[Serializable]
public class AuthenticationResponse
{
    public string accessToken;
    public string refreshToken;
    public string tokenType;
    public User User;
}