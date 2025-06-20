Feature: GameManagement

To create a game, you need a valid player id
If no valid player id is given, game does not 
get created

Scenario: Create a game
	Given you have a player with id 1222
	When user creates a game
	Then the game is created


Scenario: Try create game with no valid id
	Given you have a player without id
	When user creates a game
	Then no game is created and throws error



