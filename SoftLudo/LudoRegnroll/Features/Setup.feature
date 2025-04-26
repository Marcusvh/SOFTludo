Feature: Setup
  As a game system
  I want to set up the Ludo game correctly
  So that players can start playing

  Scenario: Game board initialization
    When a new game is created
    Then the board should have 40 regular spaces
    And the board should have 4 home areas with 4 spaces each
    And the board should have 4 home lanes with 5 spaces each
    And the board should have 4 goal areas with 4 spaces each

  Scenario: Game with 2 players
    When 2 players join the game
    Then each player should be assigned a unique color
    And each player should have 4 game pieces of their color
    And all game pieces should start in their respective home areas

  Scenario: Game with 3 players
    When 3 players join the game
    Then each player should be assigned a unique color
    And each player should have 4 game pieces of their color
    And all game pieces should start in their respective home areas

  Scenario: Game with 4 players
    When 4 players join the game
    Then each player should be assigned a unique color
    And each player should have 4 game pieces of their color
    And all game pieces should start in their respective home areas

  Scenario: Determining first player
    Given all players have joined the game
    When each player rolls the dice
    Then the player with the highest roll should take the first turn
    And in case of a tie, those players should roll again

  Scenario: Game is ready to start
    Given the board is set up
    And all players have joined
    And the starting player has been determined
    Then the game should be ready to start