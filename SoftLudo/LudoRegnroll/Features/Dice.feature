Feature: Dice
  As a player
  I want to roll a dice
  So that I can move my game pieces around the board

  Scenario: Roll a dice
    When I roll the dice
    Then the result should be between 1 and 6

  Scenario: Roll a dice multiple times
    When I roll the dice 10 times
    Then all results should be between 1 and 6

  Scenario: Roll a dice to determine player turn
    Given multiple players are ready to play
    When each player rolls the dice
    Then the player with the highest roll should start

  Scenario: Roll a six to get a game piece out
    Given I have a game piece in the home area
    When I roll a six
    Then I should be able to move the game piece to the starting position

  Scenario: Roll a six gives an extra turn
    Given it is my turn
    When I roll a six
    Then I should get another turn
