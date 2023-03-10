import { FC, Fragment } from "react";
import { useTodos } from "./api";
import "./App.css";
import TodosList from "./components/TodosList";

const App: FC = () => {
  const { todos, isLoading, isSuccess, isError } = useTodos();

  return (
    <Fragment>
      <h1>Todos</h1>
      <TodosList
        todos={todos}
        isLoading={isLoading}
        isSuccess={isSuccess}
        isError={isError}
      ></TodosList>
    </Fragment>
  );
};

export default App;
