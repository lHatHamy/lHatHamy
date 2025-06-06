import { Routes } from '@angular/router';
import { HomeComponent } from './paginas/home/home.component';
import { DeportesComponent } from './paginas/deportes/deportes.component';
import { NoticiasComponent } from './paginas/noticias/noticias.component';
import { TecnologiaComponent } from './paginas/tecnologia/tecnologia.component';
import path from 'path';


export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'deportes', component: DeportesComponent},
    {path: 'noticias', component: NoticiasComponent},
    {path: 'tecnologia', component: TecnologiaComponent}
];
