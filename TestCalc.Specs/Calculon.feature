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
