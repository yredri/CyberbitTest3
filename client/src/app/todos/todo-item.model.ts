export interface TodoItem{
    id: number;
    name: string;
    dueDate: Date;
    completed: boolean;
    status?: 'late' | 'ontime' | null;
}