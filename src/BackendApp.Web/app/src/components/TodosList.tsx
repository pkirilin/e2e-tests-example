import React, { FC, Fragment } from "react";
import { TodoItem } from "../api/contracts";
import TodosListItem from "./TodosListItem";

type TodosListProps = {
  todos?: TodoItem[];
  isLoading: boolean;
  isSuccess: boolean;
  isError: boolean;
};

const TodosList: FC<TodosListProps> = ({
  todos,
  isLoading,
  isSuccess,
  isError,
}) => {
  if (isLoading) {
    return <div role="progressbar">Loading todos...</div>;
  }

  if (isError) {
    return <div>Error while loading todos</div>;
  }

  if (isSuccess && todos && todos.length > 0) {
    return (
      <ul>
        {todos.map((todo) => (
          <TodosListItem key={todo.id} todo={todo}></TodosListItem>
        ))}
      </ul>
    );
  }

  return <div>Todo list is empty</div>;
};

export default TodosList;
