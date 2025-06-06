import { Component } from '@angular/core';
import { HeaderComponent } from '../../componentes/header/header.component';
import { AddComponent } from '../../componentes/add/add.component';

@Component({
  selector: 'app-tecnologia',
  imports: [HeaderComponent, AddComponent],
  templateUrl: './tecnologia.component.html',
  styleUrl: './tecnologia.component.css'
})
export class TecnologiaComponent {

}
