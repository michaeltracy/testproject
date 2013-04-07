Feature: Calculon
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given I started calculon

Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I press "+"
	And I have entered 70 into the calculator
	When I press "="
	Then the result should be 120 on the screen

Scenario: Subtract two numbers
	Given I have entered 70 into the calculator
	And I press "-"
	And I have entered 50 into the calculator
	When I press "="
	Then the result should be 20 on the screen

Scenario: Add two numbers then subtract one
	Given I have entered 70 into the calculator
	And I press "+"
	And I have entered 50 into the calculator
	And I press "-"
	And I have entered 20 into the calculator
	When I press "="
	Then the result should be 100 on the screen