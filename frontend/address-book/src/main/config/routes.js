import Utils from "src/auxiliaries/Utils";

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