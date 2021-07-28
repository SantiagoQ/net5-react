import AppContext from './auxiliaries/AppContext';
import { StylesProvider, jssPreset, createGenerateClassName } from '@material-ui/styles';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import Provider from 'react-redux/es/components/Provider';
import MomentUtils from '@date-io/moment';
import { Router } from 'react-router-dom';
import history from "./auxiliaries/history";
import { ThemeProvider } from '@material-ui/styles';
import Theme from "./auxiliaries/theme";
import BaseLayout from "./main/layout/BaseLayout";
import { create } from 'jss';
import routes from "./main/config/routes";
import store from "./store";

const jss = create({
  ...jssPreset(),
});
const generateClassName = createGenerateClassName();

function App() {
  return (
    <AppContext.Provider value={{ routes }}>
      <StylesProvider jss={jss} generateClassName={generateClassName}>
        <Provider store={store}>
          <MuiPickersUtilsProvider utils={MomentUtils}>
            <Router history={history}>
              <ThemeProvider theme={Theme}>
                <BaseLayout />
              </ThemeProvider>
            </Router>
          </MuiPickersUtilsProvider>
        </Provider>
      </StylesProvider>
    </AppContext.Provider>
  );
}

export default App;
