Feature: GamePiece
  As a player
  I want to move my game pieces on the board
  So that I can progress in the game

  Scenario: Starting position for a game piece
    Given I have a game piece in the home area
    And I roll a six
    When I move the game piece out
    Then the game piece should be on the starting position for my color

  Scenario: Moving a game piece
    Given I have a game piece on the board
    And I roll a 4
    When I move the game piece
    Then the game piece should move 4 positions forward

  Scenario: Game piece completing a full round
    Given I have a game piece that has moved almost a full round
    And I roll the exact number needed to enter the home lane
    When I move the game piece
    Then the game piece should enter the home lane

  Scenario: Game piece landing on opponent's piece
    Given I have a game piece on the board
    And an opponent has a game piece on a regular space
    When I land on the opponent's piece
    Then the opponent's piece should return to their home area

  Scenario: Moving a game piece to the goal
    Given I have a game piece in the home lane
    And I roll the exact number needed to reach the goal
    When I move the game piece
    Then the game piece should reach the goal