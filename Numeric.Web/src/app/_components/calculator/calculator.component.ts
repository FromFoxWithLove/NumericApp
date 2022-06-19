import { Component, OnChanges, OnInit } from '@angular/core';
import { CalculatorService } from 'src/app/_services/calculator/calculator.service';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit{

  constructor(private calculatorService: CalculatorService) { }

  expression: string = ''

  isNotExpression: boolean = false;

  ngOnInit(): void {
  }

  addCharacter(character: string) {
    if (this.isNotExpression) {
      this.expression = character;
      this.isNotExpression = false;
    }
    else {
      this.expression = this.expression + character;
    }
  }

  clear() {
    this.expression = '';
  }

  removeCharacter() {
    if (this.isNotExpression) {
      this.clear()
    }
    else {
      if (this.expression[this.expression.length - 1] == ' ') {
        this.expression = this.expression.slice(0, this.expression.length - 3);
      }
      else {
        this.expression = this.expression.toString();
        this.expression = this.expression.slice(0, this.expression.length - 1);
      }
    }
  }

  calculate() {
    this.calculatorService.Calculate(this.expression).subscribe(response => {
      this.clear();
      this.expression = response;
      let parsedResponse = parseFloat(response);
      if (isNaN(parsedResponse)) {
        this.isNotExpression = true;
      }
    }, error => {
      console.log(error);
      this.isNotExpression = true;
      this.expression = error.error.message;
    })
  }

}
