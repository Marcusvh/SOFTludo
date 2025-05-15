namespace LudoModels;
public enum ErrorType
{
    None = 0,
    PlayerNotFound = 1,
    GameNotFound = 2,
    InvalidUsername = 3,
    Unauthorized = 4,
    NotStartable = 5,
    NotEnoughPlayers = 6,
    PlayerAlreadyInGame = 7,
}
