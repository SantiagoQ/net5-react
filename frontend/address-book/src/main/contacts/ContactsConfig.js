import ContactsView from './ContactsView';

export const ContactsConfig = {
    routes: [
        {
            path: '/contacts',
            component: ContactsView,
            exact: true
        }
    ]
};