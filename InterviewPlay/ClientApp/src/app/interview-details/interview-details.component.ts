import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-interview-details',
  templateUrl: './interview-details.component.html',
  styleUrls: ['./interview-details.component.css']
})
export class InterviewDetailsComponent {
  public surveydetails: SurveyDetails[];
  public baseUri = '' as string;
  public respondentAnswers: RespondentAnswer[] = [];
  public isFinished: boolean;
  public noAnswers: boolean;
  public respondentId = '' as string;
  public interviewStarted: boolean;
  public invalidRespondentId: boolean;

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUri = baseUrl;
  }

  respondentIdGiven(id: string){
    this.respondentId = id;
  }

  start() {
    this.http.get<string>(this.baseUri + 'api/RespondentId/GenerateId').subscribe(result => {
      this.respondentId = result;
      this.interviewStarted = true;
    }, error => console.error(error));
  }

  continue() {
    var pattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
    if (this.respondentId !== '' && pattern.test(this.respondentId) === true) {
      this.interviewStarted = true;
      this.invalidRespondentId = false;
    } else
    {
      this.invalidRespondentId = true;
    }   
  }

  //Set page based on language selected
  language(lan: string){
    this.http.get<SurveyDetails[]>(this.baseUri + 'api/Interview/InterviewDetails/' + lan + '/' + this.respondentId).subscribe(result => {
      if (result === null) {
        this.isFinished = true;
      } else {
        this.surveydetails = result;
      }      
    }, error => console.error(error));
  }

  onSelect(selectedAnswerJson: string, ) {
    let answer: RespondentAnswer = JSON.parse(selectedAnswerJson.replace(/'/g, '"'));
    answer.respondentId = this.respondentId;
    this.respondentAnswers.push(answer);
    this.noAnswers = false;
  }

  openAnswerGiven(questionnaireId: number, subjectId: number, questionId: number, answer: string)
  {
    let respondentOpenAnswer = {} as RespondentAnswer;
    respondentOpenAnswer.respondentId = this.respondentId;
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
  respondentId: string;
  surveyId: number;
  subjectId: number;
  questionId: number;
  categoryId: number;
  openAnswer: string;
}

