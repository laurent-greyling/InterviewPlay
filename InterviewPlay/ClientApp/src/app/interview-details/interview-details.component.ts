import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-interview-details',
  templateUrl: './interview-details.component.html'
})
export class InterviewDetailsComponent {
  public forecasts: InterviewDetails[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<InterviewDetails[]>(baseUrl + 'api/Interview/InterviewDetails').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface InterviewDetails {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
