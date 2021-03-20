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
    template: `<div class="page-header">
        <h1> Список покупок </h1>
    </div>
    <div class="panel">
        <div class="form-inline">
            <div class="form-group">
                <div class="col-md-8">
                    <input class="form-control" [(ngModel)]="text" placeholder = "Название" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <input type="number" class="form-control" [(ngModel)]="price" placeholder="Цена" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-8">
                    <button class="btn btn-default" (click)="addItem(text, price)">Добавить</button>
                </div>
            </div>
        </div>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Предмет</th>
                    <th>Цена</th>
                    <th>Куплено</th>
                </tr>
            </thead>
            <tbody>
                <tr *ngFor="let item of items">
                    <td>{{item.purchase}}</td>
                    <td>{{item.price}}</td>
                    <td><input type="checkbox" [(ngModel)]="item.done" /></td>
                </tr>
            </tbody>
        </table>
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


}
