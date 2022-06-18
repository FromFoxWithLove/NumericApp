import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  baseUrl: string = environment.apiUrl + 'calculator'

  constructor(private http: HttpClient) { }

  Calculate(expression: string) {
    return this.http.put<string>(this.baseUrl, { expression: expression });
  }
}
