import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-interview-details',
  templateUrl: './interview-details.component.html'
})
export class InterviewDetailsComponent {
  public surveydetails: SurveyDetails[];
  public baseUri = '' as string;
  public closedAnswer: ClosedAnswers[] = [];

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUri = baseUrl;
  }

  //Set page based on language selected
  language(lan: string){
    this.http.get<SurveyDetails[]>(this.baseUri + 'api/Interview/InterviewDetails/' + lan).subscribe(result => {
      this.surveydetails = result;
    }, error => console.error(error));
  }

  onSelect(selectedAnswerJson: string, ) {
    let answer: ClosedAnswers = JSON.parse(selectedAnswerJson.replace(/'/g, '"'));
    this.closedAnswer.push(answer);
  }

  onsubmit() {
    let x = this.closedAnswer;
    var d = x;
  }
}

interface SurveyDetails {
  questionnaireId: number;
  questionnaireItems: Observable<Questionnaire[]>;
}

interface Questionnaire {
  subjectId: number;
  subjectText: string;
  questionnaireItems: Observable<QuestionnaireItems[]>;
}

interface QuestionnaireItems {
  questionId: number;
  questionItemText: string;
  answerCategoryType: number;
  questionnaireItems: Observable<Category[]>;
}

interface Category {
  answerId: number;
  categoryText: string;
}

interface ClosedAnswers {
  surveyId: number;
  subjectId: number;
  questionId: number;
  categoryId: number;
}

