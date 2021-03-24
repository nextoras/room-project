//import { Component } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpService } from './http.service';
import { User } from './user';

class Item {
    purchase: string;
    done: boolean;
    price: number;

    constructor(purchase: string, price: number) {

        this.purchase = purchase;
        this.price = price;
        this.done = false;
    }
}

@Component({
    selector: 'purchase-app',
    template: `<div>
    <style>
   h1 {
    background: #ffffff;
    padding: 40px;
    color: #000000;
   }
    h2 {
    background: #ffffff;
    padding: 20px;
    color: #000000;
   }
   p {
    background: #ffffff;
    padding: 0px;
   }
   table {
    border: 4px double #333; /* Рамка вокруг таблицы */ 
    border-collapse: separate; /* Способ отображения границы */ 
    width: 100%; /* Ширина таблицы */ 
    border-spacing: 7px 11px; /* Расстояние между ячейками */ 
   }
   td {
    padding: 5px; /* Поля вокруг текста */ 
    border: 1px solid #f52a2f; /* Граница вокруг ячеек */ 
   }
  </style>
    <div >
        <h1 style=" margin: 0 auto; text-align: center; background:  #f52a2f;">Панель управления</h1>
    
        <div class="row">
            <div class="col-md-6">
            <h2>Устройства</h2>
                
                   <table cellpadding="15" cellspacing="50">
                   
                    <tr>
                        <th>Наименование</th>
                        <th>Состояние</th>
                        <th></th>
                    </tr>
                    <tr>
                    <td>Вентилятор</td>
                        <td>True</td>
                        <input type="submit" value="Изменить" />
                    </tr> 
                    <tr>
                    <td>Настольная лампа</td>
                        <td>False</td>
                        <input type="submit" value="Изменить" />
                    </tr> 
                    <tr>
                    <td>Кондиционер</td>
                        <td>True</td>
                        <button class="btn btn-success btn-block" (click)="showNotification()">On / Off</button>
                    </tr>    
                    </table>
            </div>
            <div class="col-md-6">
            <h2>Средние значения</h2>
            <table cellpadding="15" cellspacing="50">
                   
                    <tr>
                        <th></th>
                        <th>Сейчас</th>
                        <th>За минуту</th>
                        <th>За час</th>
                        <th>За день</th>
                        <th>За неделю</th>
                        <th>За месяц</th>
                    </tr>
                    <tr>
                        <td>Термометр</td>
                        <td>24</td>
                        <td>25</td>
                        <td>24</td>
                        <td>23</td>
                        <td>24</td>
                        <td>23</td>
                    </tr>    
                    <tr>
                        <td>Влажность</td>
                        <td>80</td>
                        <td>78</td>
                        <td>82</td>
                        <td>81</td>
                        <td>80</td>
                        <td>82</td>
                    </tr>            
                    </table>
            </div>
        </div>
    </div>


</div>`,
    providers: [HttpService]
})
export class AppComponent implements OnInit {
    text: string;
    price: number = 0;
    users: User[] = [];

    constructor(private httpService: HttpService) { }

    ngOnInit() {

        this.httpService.getData().subscribe(data => this.users = data["userList"]);
    }

    items: Item[] =
        [
            { purchase: "Хлеб", done: false, price: 22 },
            { purchase: "Масло", done: false, price: 60 },
            { purchase: "Картофель", done: true, price: 22.6 },
            { purchase: "Сыр", done: false, price: 310 }
        ];
    addItem(text: string, price: number): void {

        if (text == null || text.trim() == "" || price == null)
            return;
        this.items.push(new Item(text, price));
    }
    // showNotification(){
    //     var a = this.httpService.getData();
    // }
}
