import { Component } from '@angular/core';
import { HeaderComponent } from '../../componentes/header/header.component';
import { AddComponent } from '../../componentes/add/add.component';
import { RouterLink } from '@angular/router';
import { Add2Component } from '../../componentes/add2/add2.component';

@Component({
  selector: 'app-home',
  imports: [HeaderComponent, AddComponent, Add2Component, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
