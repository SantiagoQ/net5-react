import Utils from "../../auxiliaries/Utils";
import { Redirect } from 'react-router-dom';
import { ContactsConfig } from "../contacts/ContactsConfig";

const routeConfigs = [
    ContactsConfig
];

const routes = [
    ...Utils.generateRoutesFromConfigs(routeConfigs),
    {
        path: '/',
        exact: true,
        component: () => <Redirect to="/contacts" />
    }
];

export default routes;