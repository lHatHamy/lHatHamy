import { Component } from '@angular/core';
import { HeaderComponent } from '../../componentes/header/header.component';
import { addListener } from 'node:process';
import { AddComponent } from '../../componentes/add/add.component';

@Component({
  selector: 'app-noticias',
  imports: [HeaderComponent, AddComponent],
  templateUrl: './noticias.component.html',
  styleUrl: './noticias.component.css'
})
export class NoticiasComponent {

}

