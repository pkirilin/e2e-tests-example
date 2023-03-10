import { useEffect, useState } from "react";
import { API_URL } from "../config";
import { TodoItem, TodosResponse } from "./contracts";

export function useTodos() {
  const [todos, setTodos] = useState<TodoItem[]>();
  const [isLoading, setIsLoading] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false);
  const [isError, setIsError] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        setIsLoading(true);
        const response = await fetch(`${API_URL}/api/todos`);
        const todosResponse = (await response.json()) as TodosResponse;
        setTodos(todosResponse.todos);
        setIsLoading(false);
        setIsSuccess(true);
      } catch (error) {
        setIsError(true);
      }
    })();
  }, [setIsLoading, setTodos, setIsError]);

  return {
    todos,
    isLoading,
    isSuccess,
    isError,
  };
}
