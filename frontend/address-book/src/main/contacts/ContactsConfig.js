import IniciativasView from './IniciativasView';
import { authRoles } from 'app/@theme/auth';

export const ContactsConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth: authRoles.adminHerramientas,
    routes: [
        {
            path: '/herramientas/iniciativas',
            component: IniciativasView,
            exact: true
        }
    ]
};