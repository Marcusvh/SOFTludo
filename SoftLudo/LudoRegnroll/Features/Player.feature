Feature: Player
  As a player
  I want to take turns and make moves
  So that I can participate in the Ludo game

  Scenario: Player has four game pieces
    Given a new game is set up
    When the player joins the game
    Then the player should have 4 game pieces of their color

  Scenario: Player takes a turn
    Given it is the player's turn
    When the player rolls the dice
    Then the player should be able to move a game piece according to the dice result

  Scenario: Player wins the game
    Given a player has 3 game pieces in the goal
    And the player's last game piece is one space away from the goal
    When the player rolls a 1
    Then the player should win the game

  Scenario: Player cannot move any piece
    Given a player has all game pieces in the home area
    When the player rolls a number other than 6
    Then the player's turn should end without moving any piece

  Scenario: Player must choose which piece to move
    Given a player has multiple valid moves available
    When it is the player's turn to move
    Then the player should be able to select which game piece to move

  Scenario: Player turn order
    Given there are 4 players in the game
    When a round of play completes
    Then each player should have had exactly one turn in the correct order