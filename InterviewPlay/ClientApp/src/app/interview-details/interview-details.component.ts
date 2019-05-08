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
  public respondentAnswers: RespondentAnswer[] = [];
  public isFinished: boolean;
  public noAnswers: boolean;

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
    let answer: RespondentAnswer = JSON.parse(selectedAnswerJson.replace(/'/g, '"'));
    this.respondentAnswers.push(answer);
    this.noAnswers = false;
  }

  openAnswerGiven(questionnaireId: number, subjectId: number, questionId: number, answer: string)
  {
    let respondentOpenAnswer = {} as RespondentAnswer;
    respondentOpenAnswer.surveyId = questionnaireId;
    respondentOpenAnswer.subjectId = subjectId;
    respondentOpenAnswer.questionId = questionId;
    respondentOpenAnswer.openAnswer = answer;
    this.respondentAnswers.push(respondentOpenAnswer);
  }

  onsubmit() {
    let answers = this.respondentAnswers;
    let url = this.baseUri + 'api/RespondentData/PostResponse';

    if (answers.length > 0) {
      this.http.post(url, answers).subscribe(result => {
        this.isFinished = true;
        this.noAnswers = false;
      }, error => { })
    } else {
      this.noAnswers = true;
    }
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

interface RespondentAnswer {
  surveyId: number;
  subjectId: number;
  questionId: number;
  categoryId: number;
  openAnswer: string;
}

