import Utils from "../../auxiliaries/Utils";
import { Redirect } from 'react-router-dom';

const routeConfigs = [

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