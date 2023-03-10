import { FC } from "react";
import { TodoItem } from "../api/contracts";

type TodosListItemProps = {
  todo: TodoItem;
};

const TodosListItem: FC<TodosListItemProps> = ({ todo }) => {
  return <li>{todo.title}</li>;
};

export default TodosListItem;
