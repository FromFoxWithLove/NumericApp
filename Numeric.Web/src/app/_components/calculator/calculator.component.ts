import { Component, OnChanges, OnInit } from '@angular/core';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit{

  constructor() { }

  expression: string = ''

  ngOnInit(): void {
  }

  addCharacter(character: string) {
    this.expression = this.expression + character;
  }

  clear() {
    this.expression = '';
  }

  removeCharacter() {
    if (this.expression[this.expression.length - 1] == ' ') {
      this.expression = this.expression.slice(0, this.expression.length - 3);
    }
    else {
      this.expression = this.expression.slice(0, this.expression.length - 3);
    }
  }

}
