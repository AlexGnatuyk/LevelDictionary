import {HttpClient} from "@angular/common/http";
import {Component, Inject} from "@angular/core";

@Component({
  selector: 'dictionary',
  templateUrl: './dictionary.component.html'
})
export class DictionaryComponent {
  word: string = "";
  level: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  SearchLevel(): void {
    this.http.get<string>(this.baseUrl + 'Words/GetLevelByWord?word='+ this.word).subscribe(result => {
      this.level = result;
    }, error => {
      console.error(error);
      this.level = "Неизвестно"
    });
  }
}
