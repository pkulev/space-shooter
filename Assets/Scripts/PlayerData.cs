using System;


public class PlayerData {
    private static readonly string _autogen_prefix = "Player#";

    public static string GenerateRandomName() {
        return _autogen_prefix + Guid.NewGuid().ToString("n").Substring(0, 8);
    }
}
