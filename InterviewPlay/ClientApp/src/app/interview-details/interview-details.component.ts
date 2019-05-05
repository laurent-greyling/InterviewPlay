import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-interview-details',
  templateUrl: './interview-details.component.html'
})
export class InterviewDetailsComponent {
  public surveydetails: SurveyDetails[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<SurveyDetails[]>(baseUrl + 'api/Interview/InterviewDetails').subscribe(result => {
      this.surveydetails = result;
    }, error => console.error(error));
  }
}

interface SurveyDetails {
  questionnaireId: number;
  questionnaireItems: Observable<Questionnaire[]>;
}

interface Questionnaire {
  subjectId: number;
  subjectText: string;
}
