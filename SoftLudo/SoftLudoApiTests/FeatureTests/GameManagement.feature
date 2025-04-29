Feature: GameManagement

A short summary of the feature

@createGame
Scenario: Create a game
	Given you have a player with id 1222
	When user creates a game
	Then the game is created
